using LoggerLog4net;
using OptionPricingDAO.Common;
using OptionPricingDAO.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace OptionPricingDAO
{
    public interface IOptionDAO
    {
        void InsertOptionParameters(OptionParametersDTO optionDTO);
        void InsertPrice(PriceDTO price);
        List<OptionParametersDTO> GetAllOptions();

        List<PriceDTO> GetAllPrices();

        double? GetPriceByOptionAndPricingModel(OptionParametersDTO optionParameters, PricingModelEnum pricingModel);
        void DeleteOption(OptionParametersDTO optionDTO, PricingModelEnum pricingModel);
        void DeletePrice(PriceDTO price);
    }
    public class OptionDAO : IOptionDAO
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string CONNECTION_STRING = @"Data Source=DESKTOP-E7PBEAG\SQLEXPRESS;Initial Catalog=C-sharp-training;Integrated Security=True";

        public void InsertOptionParameters(OptionParametersDTO optionDTO)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPInsertOptionParameters", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@optionType", optionDTO.OptionType.Value);
                cmd.Parameters.AddWithValue("@strike", optionDTO.Strike);
                cmd.Parameters.AddWithValue("@riskFreeRate", optionDTO.RiskFreeRate);
                cmd.Parameters.AddWithValue("@maturity", MaturityToSqlDateTime(optionDTO.Maturity));
                cmd.Parameters.AddWithValue("@volatility", optionDTO.Volatility);
                cmd.Parameters.AddWithValue("@underlying", optionDTO.Underlying);
                cmd.Parameters.AddWithValue("@spot", optionDTO.Spot);
                cmd.Parameters.AddWithValue("@product", optionDTO.UnderlyingType.Value);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    logger.Error($"Error while running InsertOptionParameters {e}");
                }
            }
        }

        public void InsertPrice(PriceDTO price)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPInsertPrice", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@price", price.Price);
                cmd.Parameters.AddWithValue("@model", price.Model.Value);
                cmd.Parameters.AddWithValue("@optionType", price.ContractType.Value);
                cmd.Parameters.AddWithValue("@strike", price.Strike);
                cmd.Parameters.AddWithValue("@riskFreeRate", price.RiskFreeRate);
                cmd.Parameters.AddWithValue("@maturity", MaturityToSqlDateTime(price.Maturity));
                cmd.Parameters.AddWithValue("@volatility", price.Volatility);
                cmd.Parameters.AddWithValue("@underlying", price.Underlying);
                cmd.Parameters.AddWithValue("@spot", price.Spot);
                cmd.Parameters.AddWithValue("@product", price.UnderlyingType.Value);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    logger.Error($"Error while running InsertPrice {e}");
                }
            }
        }

        public List<PriceDTO> GetAllPrices()
        {
            List<PriceDTO> result = new List<PriceDTO>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("select * from [dbo].[VWOptionPrice]", connection);
                try
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            double strike = (double)rdr["strike"];
                            double riskFreeRate = (double)rdr["riskFreeRate"];
                            DateTime maturity = (DateTime)rdr["maturity"];
                            double volatility = (double)rdr["volatility"];
                            string udlName = (string)rdr["underlyingName"];
                            ContractEnum contractType = ContractEnum.FromString((string)rdr["OptionType"]);
                            double spot = (double)rdr["spot"];
                            UnderlyingTypeEnum udlType = UnderlyingTypeEnum.FromString((string)rdr["productType"]);
                            double? priceValue = null;
                            if ((object)DBNull.Value != rdr["Price"])
                            {
                                priceValue = (double?)rdr["Price"];
                            }
                            PricingModelEnum pricingModel = PricingModelEnum.UNKNOWN;
                            if ((object)DBNull.Value != rdr["pricingModel"])
                            {
                                pricingModel = PricingModelEnum.FromString((string)rdr["pricingModel"]);
                            }
                            OptionParametersDTO option = new OptionParametersDTO(contractType, strike, riskFreeRate, maturity, volatility, udlName, spot, udlType);
                            PriceDTO price = new PriceDTO(option, priceValue, pricingModel);
                            result.Add(price);
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error($"Error while running GetAllPrices {e}");
                }
            }
            return result;
        }
        public List<OptionParametersDTO> GetAllOptions()
        {
            List<OptionParametersDTO> result = new List<OptionParametersDTO>();
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPGetAllOptions", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            double strike = (double)rdr["strike"];
                            double riskFreeRate = (double)rdr["riskFreeRate"];
                            DateTime maturity = (DateTime)rdr["maturity"];
                            double volatility = (double)rdr["volatility"];
                            string udlName = (string)rdr["underlyingName"];
                            ContractEnum contractType = ContractEnum.FromString((string)rdr["OptionType"]);
                            double spot = (double)rdr["spot"];
                            UnderlyingTypeEnum udlType = UnderlyingTypeEnum.FromString((string)rdr["productType"]);
                            OptionParametersDTO option = new OptionParametersDTO(contractType, strike, riskFreeRate, maturity, volatility, udlName, spot, udlType);
                            result.Add(option);
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error($"Error while running GetAllOptions {e}");
                }
            }
            return result;
        }


        public double? GetPriceByOptionAndPricingModel(OptionParametersDTO optionParameters, PricingModelEnum pricingModel)
        {
            //SPGetPriceByOptionAndPricingModel
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPGetPriceByOption", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@optionType", optionParameters.OptionType.Value);
                cmd.Parameters.AddWithValue("@strike", optionParameters.Strike);
                cmd.Parameters.AddWithValue("@riskFreeRate", optionParameters.RiskFreeRate);
                cmd.Parameters.AddWithValue("@maturity", MaturityToSqlDateTime(optionParameters.Maturity));
                cmd.Parameters.AddWithValue("@volatility", optionParameters.Volatility);
                cmd.Parameters.AddWithValue("@underlying", optionParameters.Underlying);
                cmd.Parameters.AddWithValue("@spot", optionParameters.Spot);
                cmd.Parameters.AddWithValue("@underlyingType", optionParameters.UnderlyingType.Value);
                cmd.Parameters.AddWithValue("@pricingModel", pricingModel.Value);
                try
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var res = rdr["Price"];
                            if (res != null)
                            {
                                return double.Parse(rdr["Price"].ToString());
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error($"Error while running GetPriceByOptionAndPricingModel {e}");
                }
                return null;
            }
        }

        public void DeleteOption(OptionParametersDTO optionDTO, PricingModelEnum pricingModel)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPDeleteOption", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@optionType", optionDTO.OptionType.Value);
                cmd.Parameters.AddWithValue("@strike", optionDTO.Strike);
                cmd.Parameters.AddWithValue("@riskFreeRate", optionDTO.RiskFreeRate);
                cmd.Parameters.AddWithValue("@maturity", MaturityToSqlDateTime(optionDTO.Maturity));
                cmd.Parameters.AddWithValue("@volatility", optionDTO.Volatility);
                cmd.Parameters.AddWithValue("@underlying", optionDTO.Underlying);
                cmd.Parameters.AddWithValue("@spot", optionDTO.Spot);
                cmd.Parameters.AddWithValue("@underlyingType", optionDTO.UnderlyingType.Value);
                cmd.Parameters.AddWithValue("@pricingModel", (pricingModel == null || pricingModel == PricingModelEnum.UNKNOWN) ?
                                                             (object)DBNull.Value : pricingModel.Value);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    logger.Error($"Error while running DeleteOption {e}");
                }
            }
        }

        public void DeletePrice(PriceDTO price)
        {
            using (SqlConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SPDeletePrice", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@price", price.Price);
                cmd.Parameters.AddWithValue("@model", price.Model.Value);
                cmd.Parameters.AddWithValue("@optionType", price.ContractType.Value);
                cmd.Parameters.AddWithValue("@strike", price.Strike);
                cmd.Parameters.AddWithValue("@riskFreeRate", price.RiskFreeRate);
                cmd.Parameters.AddWithValue("@maturity", MaturityToSqlDateTime(price.Maturity));
                cmd.Parameters.AddWithValue("@volatility", price.Volatility);
                cmd.Parameters.AddWithValue("@underlying", price.Underlying);
                cmd.Parameters.AddWithValue("@spot", price.Spot);
                cmd.Parameters.AddWithValue("@product", price.UnderlyingType.Value);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    logger.Error($"Error while running DeletePrice {e}");
                }
            }
        }

        private SqlDateTime MaturityToSqlDateTime(DateTime date)
        {
            return new SqlDateTime(date.Year, date.Month, date.Day);
        }
    }
}
