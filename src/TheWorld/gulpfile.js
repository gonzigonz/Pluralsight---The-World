/// <binding AfterBuild='minify' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var uglify = require('gulp-uglify');
var ngAnnotate = require('gulp-ng-annotate');

gulp.task('minify', function () {
	return gulp.src('wwwroot/js/*.js')
		.pipe(ngAnnotate())
		.pipe(uglify())
		.pipe(gulp.dest('wwwroot/lib/_app'));
});