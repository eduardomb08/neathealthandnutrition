
namespace Neat.Health.DietAndFitness
{
    public class Person
    {
        public Gender Gender { get; set; }
        public decimal Height { get; set; }
        public HeightUnit HeightUnit { get; set; } = HeightUnit.Centimeters;
        public decimal Weight { get; set; }
        public WeightUnit WeightUnit { get; set; } = WeightUnit.Kilograms;
        public int Age { get; set; }
        public BodyType BodyType { get; set; }
        public ActivityLevel ActivityLevel { get; set; }

        public decimal CalculateBMR()
        {
            decimal heightInCm = ConvertHeightToCentimeters();
            decimal weightInKg = ConvertWeightToKilograms();

            decimal bmr;

            switch (Gender)
            {
                case Gender.Male:
                    bmr = 66 + (6.23m * weightInKg) + (12.7m * heightInCm) - (6.8m * Age);
                    break;
                case Gender.Female:
                    bmr = 655 + (4.35m * weightInKg) + (4.7m * heightInCm) - (4.7m * Age);
                    break;
                default:
                    throw new InvalidOperationException("Invalid gender specified.");
            }

            return bmr;
        }

        private decimal CalculateTDEE(decimal bmr)
        {
            decimal tdee;

            switch (ActivityLevel)
            {
                case ActivityLevel.Sedentary:
                    tdee = bmr * 1.2m;
                    break;
                case ActivityLevel.LightlyActive:
                    tdee = bmr * 1.375m;
                    break;
                case ActivityLevel.ModeratelyActive:
                    tdee = bmr * 1.55m;
                    break;
                case ActivityLevel.VeryActive:
                    tdee = bmr * 1.725m;
                    break;
                case ActivityLevel.ExtremelyActive:
                    tdee = bmr * 1.9m;
                    break;
                default:
                    throw new InvalidOperationException("Invalid activity level specified.");
            }

            return tdee;
        }

        public decimal CalculateTDEE()
        {
            return CalculateTDEE(CalculateBMR());
        }

        public Dictionary<string, decimal> CalculateMacros()
        {
            decimal bmr = CalculateBMR();
            decimal tdee = CalculateTDEE(bmr);

            Dictionary<string, decimal> macros = new Dictionary<string, decimal>();

            decimal proteinRatio = 0;
            decimal carbRatio = 0;
            decimal fatRatio = 0;

            switch (BodyType)
            {
                case BodyType.Ectomorph:
                    proteinRatio = 0.3m;
                    carbRatio = 0.5m;
                    fatRatio = 0.2m;
                    break;
                case BodyType.Mesomorph:
                    proteinRatio = 0.4m;
                    carbRatio = 0.3m;
                    fatRatio = 0.3m;
                    break;
                case BodyType.Endomorph:
                    proteinRatio = 0.3m;
                    carbRatio = 0.4m;
                    fatRatio = 0.3m;
                    break;
                default :
                    proteinRatio = 1 / 3m;
                    carbRatio = 1 / 3m;
                    fatRatio = 1 / 3m;
                    break;
            }

            decimal protein = tdee * proteinRatio / 4; // 4 calories per gram of protein
            decimal carbs = tdee * carbRatio / 4; // 4 calories per gram of carbs
            decimal fats = tdee * fatRatio / 9; // 9 calories per gram of fats

            macros.Add("Protein (g)", protein);
            macros.Add("Carbs (g)", carbs);
            macros.Add("Fats (g)", fats);

            return macros;
        }

        private decimal ConvertHeightToCentimeters()
        {
            if (HeightUnit == HeightUnit.Inches)
            {
                return Height * 2.54m; // Convert inches to centimeters
            }

            return Height; // Height is already in centimeters
        }

        private decimal ConvertWeightToKilograms()
        {
            if (WeightUnit == WeightUnit.Pounds)
            {
                return Weight * 0.45359237m; // Convert pounds to kilograms
            }

            return Weight; // Weight is already in kilograms
        }
    }

}