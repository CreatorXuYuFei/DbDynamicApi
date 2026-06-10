using Model.InfoModel;

namespace DbDynamicService
{
    ///<summary>
    ///DTO转换扩展类
    ///</summary>
    public static class DtoMapperExtensions
    {
        ///<summary>
        ///接口条件 → 底层条件
        ///</summary>
        public static List<DynamicCondition> ToDynamicConditions(this List<DynamicConditionDto> dtos)
        {
            if (dtos == null || dtos.Count == 0) return [];

            return dtos.Select(d => new DynamicCondition
            {
                FieldName = d.FieldName,
                Operator = d.Operator,
                Value = d.Value,
                NextLogic = d.NextLogic
            }).ToList();
        }

        ///<summary>
        ///接口排序 → 底层排序
        ///</summary>
        public static List<DynamicOrder> ToDynamicOrders(this List<DynamicOrderDto> dtos)
        {
            if (dtos == null || dtos.Count == 0) return [];

            return dtos.Select(d => new DynamicOrder
            {
                FieldName = d.FieldName,
                IsDescending = d.IsDescending
            }).ToList();
        }
    }
}
