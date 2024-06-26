﻿using VFoody.Domain.Entities;

namespace VFoody.Application.Common.Repositories;

public interface IQuestionRepository : IBaseRepository<Question>
{
    Task<bool> CheckExistedQuestionByIdsAndProductId(List<int> questionIds, int productId);

    Task<Question?> GetQuestionIncludeOptionById(int id);
    Task<List<Question>> GetQuestionByIds(List<int> ids);
    Task<List<Question>> GetQuestionByProductId(int productId);
}
