let assert = require('chai').assert;
let jsdom = require('jsdom-global')();
let $ = require('jquery');

function nuke(selector1, selector2) {
    if (selector1 === selector2) return;
    $(selector1).filter(selector2).remove();
}

describe('Testing nuke(selector1, selector2) functionality', function() {
    beforeEach('initialize html', function() {
        document.body.innerHTML = `
        <div id="target">
            <div class="nested target">
                <p>This is some text</p>
            </div>
            <div class="target">
                <p>Empty div</p>
            </div>
            <div class="inside">
                <span class="nested">Some more text</span>
                <span class="target">Some more text</span>
            </div>
        </div>`;
    });

    it('doesn\'t do anything if first selector is invalid', function() {
        let prevHTML = document.body.innerHTML;
        nuke('#target', 5);
        let newHTML = document.body.innerHTML;
        assert(prevHTML == newHTML);
    });
    it('doesn\'t do anything if second selector is invalid', function () {
        let prevHTML = document.body.innerHTML;
        nuke('invalid', '#target');
        let newHTML = document.body.innerHTML;
        assert(prevHTML == newHTML);
    });
    it('does nothing if selector is not complete', function() {
        let selector = $('#target');
        let prevHTML = selector.html();
        nuke('.nested', '.inside');
        assert(selector.html() == prevHTML);
    });

    it('doesn\'t do anything if both selectors are the same', function () {
        let selector = '#target';
        let prevHTML = document.body.innerHTML;
        nuke(selector, selector);
        let newHTML = document.body.innerHTML;
        assert(prevHTML == newHTML, 'DOM was manipulated when both selectors are equal');
    });

    it('deletes all matching nodes from both selectors', function () {
        let selector1 = $('.target');
        let selector2 = $('.nested');
        let prevHTML = $('#target').html();
        nuke(selector1, selector2);
        assert(prevHTML != $('#target').html(), '.target selector not removed');
    });
});