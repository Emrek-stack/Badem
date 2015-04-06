module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),

        watch: {
            react: {
                files: 'app/components/*.js',
                tasks: ['browserify']
            }
        },

        browserify: {
            options: {
                transform: [require('grunt-react').browserify]
            },
            client: {
                src: ['app/components/*.js'],
                dest: 'app/prod/bundle.js'
            }
        }
    });

    grunt.loadNpmTasks('grunt-browserify');
    grunt.loadNpmTasks('grunt-contrib-watch');

    grunt.registerTask('default', [
        'browserify'
    ]);
};