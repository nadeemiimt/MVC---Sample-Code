using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EPCommon.Contracts;
using EPCommon.ViewModels;
using EPDataLayer.Entities;
using EPDataLayer.Repository;
using NLog;

namespace EPBusinessLogic
{
    public class RegisterLogic : IRegister
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public RegisterLogic(ILogger logger, IRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public List<BusinessAreaTypesModel> GetBusinessAreaTypes()
        {
            try
            {
                var result =  _repository.GetAll<BusinessAreaType>().ToList();
                return _mapper.Map<List<BusinessAreaTypesModel>>(result);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e);
                return null;
            }
        }

        public List<BusinessSegmentTypesModel> GetBusinessSegmentTypes()
        {
            try
            {
                var result = _repository.GetAll<BusinessSegmentType>().ToList();
                return _mapper.Map<List<BusinessSegmentTypesModel>>(result);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e);
                return null;
            }
        }

        public string RegisterCompany(RegisterModel register)
        {
            var message = string.Empty;
            var clientId = SaveClientAndReturnSavedId(register,ref message);
            if (clientId > 0 && string.IsNullOrWhiteSpace(message))
            {
                if (register.BusinessAreas != null && register.BusinessAreas.Count > 0)
                {
                    SaveBusinessAreas(register.BusinessAreas, clientId, ref message);
                }
            }
            return message;
        }

        private long SaveClientAndReturnSavedId(RegisterModel register, ref string error)
        {
            try
            {
                var clientData = _mapper.Map<ClientMaster>(register);
                var client = _repository.Save(clientData);
                return client.Id;
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error,e);
                error = e.Message;
                return long.MinValue;
            }
        }

        private void SaveBusinessAreas(List<ClientContactModel> contacts, long clientId,ref string error)
        {
            try
            {
                var clientContacts = _mapper.Map<List<ClientContact>>(contacts, opt => {
                    opt.Items["ClientId"] = clientId;
                });
                _repository.Add(clientContacts);
                _repository.UnitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e);
                error = e.Message;
            }

        }
    }
}
