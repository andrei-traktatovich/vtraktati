(() => {

    window.createModule("traktat.filters")
        .filter("addPlus", addPlusFilter);

    function addPlusFilter() {
        
        return (input) => {
            if (isNaN(input)) return "";
            else 
                return input <= 0 ? input.toString() : `+${input.toString()}`;
        }
    }

})();
