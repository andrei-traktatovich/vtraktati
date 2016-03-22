(() => {

    window.createModule("traktat.filters")
        .filter("addHoursToLocalTime", addHoursToLocalTimeFilter);
        
    function addHoursToLocalTimeFilter(moment) {
        return (input) => moment().add(input, "h").format("HH:mm DD.MM.YY");
    }

})();