(function (moduleName) {

    var defaults = {
        lang: 'ru',
        firstDay: 1,
        title: 'MMM',
        timezone: 'local',

        header: {
            left: 'month,agendaDay,agendaWeek',
            center: 'title',
            right: 'today prev,next'
        }
    };

    angular.module(moduleName)
    .factory('calendarDefaultOptions', function () {
        return angular.copy(defaults);
    });
})('app');
