$(() => {
    renderCatTemplate();

    async function renderCatTemplate() {
        let cats = window.cats;
        let source = await $.get('template.hbs');
        let template = Handlebars.compile(source);
        $(document.body).empty();
        $(document.body).html(template({cats}));

        $('button').click(showCode);

        function showCode() {
            $(this).next().toggle();
        }
    }

})
