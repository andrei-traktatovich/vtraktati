// dependencies: ui.router
/* example of source:

	source = [
		{ text : 'Админ', state : 'itemstate', glyph : 'itemglyph' },
		{ text : 'Менеджер', state : 'abstractstate', glyph : 'someglyph', subitems : 
			[ 
				{ text : 'item text', state : 'itemstate', glyph : 'itemglyph' },
				{ text : 'item text', state : 'itemstate', glyph : 'itemglyph' }
			]
		},
		{ text : 'login', action : 'logout()', glyph : 'itemglyph' },
	]
    (if action is specified, state is ignored !!!)
*/

angular.module('traktat.ui')
    .directive('traktatActiveNavbar', function ($state) {

        function addItems(scope, gatekeeper, el, source, level) {

            level = level || 0;

            source.forEach(function (item) {
                var nextMenuItem, authorized = (gatekeeper && item.state) ? gatekeeper.authorizeState(item.state) : true;
                if (authorized) {
                    var glyph = makeGlyphicon(item);
                    nextMenuItem = (item.subitems) ? makeSubmenu(item, level + 1, glyph) : makeMenuItem(item, glyph);
                    el.append(nextMenuItem);
                }
            });

            function makeGlyphicon(item) {
                var glyph = item.glyph;
                return glyph ? '<span class="text-info ' + item.glyph + '"/>&nbsp;' : ' ';
            }

            function makeMenuItem(item, glyph) {
                var result;

                if (!item.action)
                    result = $('<li><a href="' + $state.href(item.state) + '">' + glyph + item.text + "</a></li>");
                else {
                    result = $('<li><a href="#">' + glyph + item.text + "</a></li>");
                    result.bind('click', item.action);
                }
                
                return result;
            };

            function makeSubmenu(item, level, glyph) {
                var result = (level < 2) ?
                    $('<li></li>') :
                    $('<li class="dropdown-submenu"></li>'); // this is because I have an unexplained issue with css (wrong position of submenu's)

                result.append('<a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button">' + glyph + item.text + '</a>');
                var submenu = $("<ul class='dropdown-menu' role='menu'></ul>'");
                addItems(scope, gatekeeper, submenu, item.subitems, level);
                result.append(submenu);
                return result;
            }

        }

        return {
            scope: {
                source: '=source',
                gatekeeper: '=gatekeeper'
            },
            link: function (scope, element, attrs) {
                var root = $("<ul class='nav navbar-nav multi-level'></ul>");
                addItems(scope, scope.gatekeeper, root, scope.source);
                element.append(root);
            }
        };
    });
