using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }
        [JsonIgnore] // => Sen bunu Json'a dönüştürürken ignore et.Client bunu görmesin.Bu anlamda kullanılır.
        public int StatusCode { get; set; }
        public List<String> Errors { get; set; }
        public static CustomResponseDto<T> Success(int statusCode, T data) // Bir T değeri de döndüğünde
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Success(int statusCode) // Bir T data dönmediğinde, sadece status code oluştuğunda
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors) // Birden fazla error gelirse
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error) // Tek bir error dönerse
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error } };
        }

    }
}
