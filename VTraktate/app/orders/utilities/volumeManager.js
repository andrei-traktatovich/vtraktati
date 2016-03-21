(function () {
    angular.module('orders.utilities')
    .service('volumeManager', volumeManager);

    function volumeManager() {

        var DECIMAL_PLACES_IN_PAGES = 2,
            CHARS_IN_PAGES = 1800,
            WORDS_IN_PAGE = 250;

        return {
            updateFromChars: updateFromChars,
            updateFromPages: updateFromPages,
            updateFromWords: updateFromWords
        };

        function updateFromChars(volume, pagesRoundingRule) {
            volume.pages = pagesFromChars(volume.chars, pagesRoundingRule);
            volume.words = wordsFromPages(volume.pages);
            return volume;
        }

        function updateFromPages(volume) {
            volume.chars = charsFromPages(volume.pages);
            volume.words = wordsFromPages(volume.pages);
            return volume;
        }

        function updateFromWords(volume, pagesRoundingRule) {
            volume.pages = pagesFromWords(volume.words, pagesRoundingRule);
            volume.chars = charsFromPages(volume.pages);
            return volume;
        }

        function pagesFromChars(chars, pagesRoundingRule) {
            return isNanKa(chars, function (chars) {
                var pages = Math.round(chars / CHARS_IN_PAGES * Math.pow(10, DECIMAL_PLACES_IN_PAGES)) / 100;
                return ifFunc(pagesRoundingRule, pages);
            });
        }

        function wordsFromPages(pages) {
            return isNanKa(pages, function (pages) {
                return Math.round(pages * WORDS_IN_PAGE)
            });
        }

        function charsFromPages(pages) {
            return isNanKa(pages, function (pages) {
                return Math.round(pages * CHARS_IN_PAGES);
            });
        }

        function pagesFromWords(words, pagesRoundingRule) {
            return isNanKa(words, function (words) {
                var pages = Math.round(words) / WORDS_IN_PAGE;
                return ifFunc(pagesRoundingRule, pages);
            });
        }

        function isNanKa(value, func) {
            return isNaN(value) ? 0 : func(value);
        }

        function ifFunc(func, value) {
            return typeof func === 'function' ? func(value) : value;
        }
    }

})();