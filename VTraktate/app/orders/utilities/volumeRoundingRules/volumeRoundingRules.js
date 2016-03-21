(function () {
    angular.module('orders.utilities.volumeRoundingRules', [])
    .service('volumeRoundingRuleLocator', volumeRoundingRuleLocator);

    function volumeRoundingRuleLocator() {
        
        // in all rounding rules, amount is expected to be a valid number

        // default: if < 1 then 1 else round to nearest .5
        var defaultRoundingRule = {
            name: 'Станд.округление',
            description: 'Минимальный объем 1 стр., все, что больше, округляется до ближайшего 0,5',
            func: function (amount) {
                if (amount < 1)
                    return 1;
                return parseFloat((Math.round(amount * 2) / 2).toFixed(1));
            }
        }

        var NO_ROUNDINNG = null,

            roundingRules = {
            0 : NO_ROUNDINNG,
            1 : defaultRoundingRule
        };

        return {
            get: getRule
        };

        function getRule(ruleId) {
            if (!ruleId || !roundingRules.hasOwnProperty(ruleId))
                return null;

            var rule = roundingRules[ruleId];

            return {
                name: rule.name,
                description: rule.description,
                func: safeNumber(rule.func)
            };
        }

        function safeNumber(func) {
            return function(amount) {
                return angular.isNumber(amount) ? func(amount) : 0;
            }
        }
    }
})();