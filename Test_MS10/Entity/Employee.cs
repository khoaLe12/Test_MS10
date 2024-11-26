using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Test_MS10.Common;

namespace Test_MS10.Entity;

public class Employee
{
    [Key]
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateOnly DoB { get; set; }
    public int Position { get; set; }

    public string GetPosition()
    {
        PositionEnum positionEnum = (PositionEnum)this.Position;
        var field = positionEnum.GetType().GetField(positionEnum.ToString());
        if(field is null)
        {
            return positionEnum.ToString();
        }
        var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        if(attribute is null)
        {
            return positionEnum.ToString();
        }
        var descriptionAttribute = (DescriptionAttribute)attribute;
        return descriptionAttribute.Description;
    }
}
