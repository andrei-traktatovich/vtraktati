(function () {
    angular.module('orders.utilities')
        .service('priceCalculator', priceCalculator);

    function priceCalculator() {
        return {
            updatePrice: updatePrice
        };

        function updatePrice(volumeAndPriceInfo) {
            if (!volumeAndPriceInfo)
                throw new Error('priceCalculator: null or undefined volumeAndPriceInfor');

            if (!volumeAndPriceInfo.volume)
                throw new Error('priceCalculator: malformed or null volumeAndPriceInfor object: no volume object');

            if (!volumeAndPriceInfo.pricing)
                throw new Error('priceCalculator: malformed or null volumeAndPriceInfor object: no pricing object');
            console.log('will call getPrice with  pages = ' + volumeAndPriceInfo.volume.pages +
                ' and rate = ' + volumeAndPriceInfo.pricing.rate);

            var price = getPrice(volumeAndPriceInfo.volume.pages, volumeAndPriceInfo.pricing.rate);
            var discountedPrice = applyDiscount(price, volumeAndPriceInfo.pricing.discount);

            volumeAndPriceInfo.pricing.price = price;
            volumeAndPriceInfo.pricing.discountedPrice = discountedPrice;

            return volumeAndPriceInfo;
        }

        function getPrice(pages, rate) {
            var result = Math.round(pages * rate * 100) / 100;
            console.log('pages = ' + pages + ' rate = ' + rate + ' result = ' + result);
            return isNaN(result) ? 0 : result;
        }

        function applyDiscount(price, discount) {
            if (isNaN(discount))
                discount = 0;
            if (isNaN(price))
                price = 0;
            if (discount < 0 || discount > 100)
                throw new Error('jobPriceCalculator: invalid discount: ' + discount + '. Discount cannot be negative or exceed 100%');
            var amount = Math.round((price - (price / 100 * discount)) * 100) / 100;
            return amount;
        }
    }
})()
