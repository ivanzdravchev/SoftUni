function traverse(selector) {
    let element = $(selector);

    let bestDepth = -1;
    let bestSelector;
    deepestChild(0, element);

    function deepestChild(currentDepth, currentSelector) {
        if (bestDepth < currentDepth) {
            bestDepth = currentDepth;
            bestSelector = currentSelector;
        }

        let children = currentSelector.children();
        for (let child of children) {
            deepestChild(currentDepth + 1, $(child));
        }
    }

    while (true) {
        bestSelector.addClass('highlight');
        if (bestSelector.attr('id') == element.attr('id')) {
            return;
        }
        bestSelector = bestSelector.parent();
    }
}