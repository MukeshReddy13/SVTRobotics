using SVTRobotics.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SVTRobotics.BusinessLogic
{
    public static class CalculateDistance
    {
        public static void UpdateDistance(OutputValues data, string robotId, int batteryLevel, int targetX, int targetY, InputValues inputValues)
        {
            data.robotId = robotId;
            data.batteryLevel = batteryLevel;
            data.x = inputValues.xAxis;
            data.y = inputValues.yAxis;
            data.distance = SetDistance(targetX, targetY, inputValues.xAxis, inputValues.yAxis);
        }
        private static double SetDistance(int targetX, int targetY, int X, int Y)
        {            
            return Math.Round(Math.Sqrt((targetX - X) * (targetX - X) + (targetY - Y) * (targetY - Y)), 2);
        }

        public static OutputValues BestDistanceWithBattery(List<OutputValues> data)
        {
            OutputValues returnVal = new OutputValues();
            double efficiency = 0;
            double temp;
            double minimumDistance = data.FirstOrDefault().distance;
            foreach (var item in data)
            {
                temp = item.batteryLevel - item.distance;
                if (efficiency < temp)
                {
                    efficiency = temp;
                    returnVal = item;
                }
                if (minimumDistance + 10 <= item.distance)
                {
                    break;
                }
            }

            return returnVal;
        }

    }
}
