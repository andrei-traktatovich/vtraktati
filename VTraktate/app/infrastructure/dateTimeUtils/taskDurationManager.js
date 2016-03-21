(function () {
    angular.module('dateTimeUtils')
		.service('taskDurationManager', taskDurationManager);

    function taskDurationManager() {
        var defaultBusinessDays = '1 2 3 4 5',
			defaultOpeningHour = 9,
			defaultClosingHour = 19,
			defaultJobDuration = { days: 1 };

        return {
            defaultJobEndDate: defaultJobEndDate,
            adjustToWorkSchedule: adjustToWorkSchedule,
            adjustToBusinessDays: adjustToBusinessDays,
            jobPivotalDate: jobPivotalDate,
            adjustedPivotalDate: adjustedPivotalDate,
            roundTo10Minutes: roundTo10Minutes
        };



        function roundTo10Minutes(dt) {
            var value = moment(dt);
            value.minutes(Math.round(value.minutes() / 10) * 10);
            return value;
        }
        function adjustedPivotalDate(dtStart, dtEnd, openingHour, closingHour, businessDays) {
            var pivotal = jobPivotalDate(dtStart, dtEnd);
            var adjustedToSchedule = adjustToWorkSchedule(pivotal, openingHour, closingHour);
            if (adjustedToSchedule.isAfter(dtEnd))
                return dtEnd;
            var adjustedToBusinessDays = adjustToBusinessDays(adjustedToSchedule, businessDays);
            if (adjustedToBusinessDays.isAfter(dtEnd))
                return adjustedToSchedule;
            else
                return adjustedToBusinessDays;
        }
        function jobPivotalDate(dtStart, dtEnd) {
            dtStart = moment(dtStart);
            dtEnd = moment(dtEnd);

            var diff = dtEnd.diff(dtStart, 'hours');
            console.log('difference = ' + diff);
            var pivotal = roundTo10Minutes(moment(dtStart).add(diff / 100 * 80, 'hours'));
            var result = pivotal.isAfter(dtStart) ? pivotal : dtEnd;
            return result;
        }
        function throwIfDateInvalid(dt) {
            if (!moment(dt).isValid())
                throw new Error('Неверная дата');
        }

        function defaultJobEndDate(startDate, expectedDuration) {
            startDate = startDate || moment();
            
            var dt = roundTo10Minutes(moment(startDate));
            console.log('start daet = ' + dt);
            expectedDuration = expectedDuration || defaultJobDuration;
            var result = adjustToWorkSchedule(dt.add(expectedDuration));
            
            var adjustedResult = adjustToBusinessDays(adjustToWorkSchedule(result));
            return adjustedResult;
        }

        function adjustToWorkSchedule(value, openingHour, closingHour) {
            throwIfDateInvalid(value);
            openingHour = openingHour || defaultOpeningHour;
            closingHour = closingHour || defaultClosingHour;

            console.log('hors = ' + value.hours());

            if (value.hours() < openingHour) {
                value.hours(openingHour);
                value.minutes(0);
                value.seconds(0);
            }

            if (value.hours() > closingHour) {
                value.hours(closingHour);
                value.minutes(0);
                value.seconds(0);
            }
            console.log('adjustedValue = ' + moment(value).format('LLL'));
            return value;
        }



        function adjustToBusinessDays(value, businessDays) {
            throwIfDateInvalid(value);

            businessDays = businessDays || defaultBusinessDays;

            var businessDay;

            var arr = defaultBusinessDays.split(' ').sort(),
				currentDay = value.isoWeekday();

            if (arr.indexOf(currentDay.toString()) < 0) {

                var nextBusinessDays = arr.filter(function (item) { return item > currentDay; });
                if (nextBusinessDays.length)
                    businessDay = _.first(nextBusinessDays);
                else
                    businessDay = _.first(arr);
                value.isoWeekday(businessDay).add(7, 'days'); // next 
            }
            return value;
        }
    }
})();