using APIBIM.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIBIM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BMIController : ControllerBase
    {
        [HttpPost]
        [Route("calculateBMI")]
      

        public IActionResult CalculateBMI(BMIDTO bmi)
        {
            try { 
            if (bmi == null)
            {
                return BadRequest(new
                {
                    error = "BMI DTO cannot be null."
                });
            }
            if (bmi.Age == null)
            {
                return BadRequest(new
                {
                    error = "Please provide an age between 2 and 120"
                });
            }
            if (bmi.Age < 2 || bmi.Age > 120)
            {
                return BadRequest(new
                {
                    error = "Input data for Age is out of range!"
                });
            }
            if (bmi.Weight <= 0 || bmi.Height <= 0)
            {
                return BadRequest(new
                {
                    error = "Weight and height must be greater than 0."
                });
            }

            double bmiResult = CalculateBMIValue(bmi.Weight, bmi.Height);
            string bmiCategory = null;
            if (bmi.Age >= 20 & bmi.Age <= 120)
            {
                bmiCategory = GetBMICategoryAdult(bmiResult);
            }
            else
            {
                bmiCategory = GetBMIClassificationForChildren(bmiResult);
            }

            return Ok(new
            {
                message = "BMI calculation successful",
                weight = bmi.Weight,
                height = bmi.Height,
                gender = bmi.Gender ? "Male" : "Female",
                age = bmi.Age,
                bmiValue = bmiResult,
                category = bmiCategory
            });
            } catch
            {
                return NotFound("Đã xảy ra lỗi trong quá trình đăng nhập , vui lòng thử lại.");
            }
        }

        private double CalculateBMIValue(int weight, double height)
        {
            return Math.Round(((weight * 10000) / (height * height)), 2);
        }
        private string GetBMICategoryAdult(double bmi)
        {
            if (bmi < 16)
            {
                return "Severe Thinness";
            }
            else if (bmi < 17)
            {
                return "Moderate Thinness";
            }
            else if (bmi < 18.5)
            {
                return "Mild Thinness";
            }
            else if (bmi < 25)
            {
                return "Normal";
            }
            else if (bmi < 30)
            {
                return "Overweight";
            }
            else if (bmi < 35)
            {
                return "Obese Class I";
            }
            else if (bmi < 40)
            {
                return "Obese Class II";
            }
            else
            {
                return "Obese Class III";
            }
        }
        private string GetBMIClassificationForChildren(double bmi)
        {
            if (bmi < 5)
            {
                return "Underweight";
            }
            else if (bmi >= 5 && bmi < 85)
            {
                return "Healthy weight";
            }
            else if (bmi >= 85 && bmi < 95)
            {
                return "At risk of overweight";
            }
            else
            {
                return "Overweight";
            }
        }
    }
}
