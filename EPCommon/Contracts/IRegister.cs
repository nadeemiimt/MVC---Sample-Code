using System;
using System.Collections.Generic;
using EPCommon.ViewModels;

namespace EPCommon.Contracts
{
    public interface IRegister
    {
        /// <summary>
        /// Get All Business Area
        /// </summary>
        /// <returns></returns>
        List<BusinessAreaTypesModel> GetBusinessAreaTypes();

        /// <summary>
        /// Get All business Segment
        /// </summary>
        /// <returns></returns>
        List<BusinessSegmentTypesModel> GetBusinessSegmentTypes();

        /// <summary>
        /// Register new Company
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>

        string RegisterCompany(RegisterModel register);
    }
}
