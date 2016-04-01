(() => {
    "use strict";

    var buttons = [
        { name: "cancel", glyph: "erase" },
        { name: "edit", glyph: "pencil" },
        { name: "save", glyph: "floppy-save" },
        { name: "inc", glyph: "plus" },
        { name: "dec", glyph: "minus" },
        { name: "load", glyph: "floppy-open" },
        { name: "reset", glyph: "unchecked" }
    ];

    buttons.forEach(makeDirective);

    function makeDirective(item) {
        var glyphName = item.glyph || item.name;
        var directiveName = `trkBtn${toProper(item.name)}`;
        
        var directive = () => {
            return {
                replace: true,
                template: `<button class="btn btn-sm"><span class="glyphicon glyphicon-${ glyphName }"><span></button>`
        }
        };

        angular.module("traktat.ui.standardButtons")
            .directive(directiveName, directive);

    }

    function toProper(string) {
        return string.replace(/\w\S*/g, function(txt){return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase();});
    }

})();