﻿using AutoMapper;
using Domains.Entities;
using WillsPlatform.Application;
using WillsPlatform.Application.DTOs;
using WillsPlatform.Application.Repositories;
using WillsPlatform.Application.Services;
using WillsPlatform.Infrastructure.Repositories;

namespace WillsPlatform.Infrastructure.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public QuestionService(IQuestionRepository questionRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsAsync()
        {
            var questions = await _questionRepository.GetAllQuestionsAsync();
            return _mapper.Map<List<QuestionDTO>>(questions);
        }

        public async Task<bool> AddQuestionAsync(QuestionDTO questionPostDTO)
        {
            try
            {
                var question = _mapper.Map<Question>(questionPostDTO);
                await _questionRepository.AddAsync(question);
                var addedRecord = await _unitOfWork.SaveChangesAsync();
                return (addedRecord > 0);
            }
            catch (Exception ex) 
            { 
                return false;
            }
        }

        public async Task<QuestionDTO> GetQuestionsByIdAsync(int id)
        {
            var questions = await _questionRepository.GetAsync(id);
            return _mapper.Map<QuestionDTO>(questions);
        }

        public async Task<bool> UpdateQuestionAsync(QuestionDTO questionPostDTO)
        {
            try
            {
                var question = await _questionRepository.GetAsync(questionPostDTO.Id);
                question.Text = questionPostDTO.Text;
                question.FieldId = questionPostDTO.FieldId;
                question.FormId = questionPostDTO.FormId;
                await _questionRepository.UpdateAsync(question);
                var updatedRecord = await _unitOfWork.SaveChangesAsync();
                return (updatedRecord > 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> DeleteQuestionAsync(int id)
        {
            try
            {
                _questionRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
