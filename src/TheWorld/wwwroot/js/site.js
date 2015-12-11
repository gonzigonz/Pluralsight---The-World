// site.js

var ele = document.getElementById('username');
ele.innerHTML = 'Freddy Junior';

var main = document.getElementById('main');
main.onmouseenter = function () {
	// Firefox version
	//main.style = 'background-color: #888;';

	// IE & Google Chrome version
	main.style['background-color'] = '#888';
	
};
main.onmouseleave = function () {
	// Firefox Version
	//main.style = '';

	// IE & Google Chrome version
	main.style['background-color'] = '';
	
};