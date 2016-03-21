angular.module('traktat.ui')
.value('GlyphiconService', {

    // типы ресурсов
    'сотрудник': 'glyphicon glyphicon-globe text-success',
    'фрилансер': 'glyphicon glyphicon-globe text-warning',
    'подрядчик': 'glyphicon glyphicon-globe text-danger',

    // валюты
    'рубль': 'glyphicon glyphicon-rub',
    'доллар сша': 'glyphicon glyphicon-usd',
    'евро': 'glyphicon glyphicon-eur',

})
.directive('trkGlyphicon', function (GlyphiconService) {
    
    function getClasses(text) {
        if (!text)
            return '';
        else {
            var item = typeof text === 'string' ? text.trim().toLowerCase() : item;
            return GlyphiconService[item];
        }
    }

    return {
        scope: {
            text: '=trkGlData',    
        },
        templateUrl: 'app/common/directives/traktat.ui/trkGlyphicon/trkGlyphicon.tpl.html',
        controller: function ($scope) {
             
            $scope.classes = getClasses($scope.text);
        }

    };
})