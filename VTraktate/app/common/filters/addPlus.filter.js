(() => {

    window.createModule("traktat.filters")
        .filter("addPlus", addPlusFilter);

    function addPlusFilter() {
        return (input) => input <= 0 ? input.toString() : "+" + input.toString();
    }

})();