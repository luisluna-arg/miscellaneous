/* Crossbrowser clipboard */

if (window.clipboardData !== undefined && window.clipboardData.setData !== undefined) {
    /* En internet explorer */
    window.clipboardData.setData('Text', byId("text_container_dom").innerHTML);
}
else {
    /* En chrome */
    function selectElementContents(el) {
        var range = document.createRange();
        range.selectNodeContents(el);
        var sel = window.getSelection();
        sel.removeAllRanges();
        sel.addRange(range);
    }
    selectElementContents(document.getElementById("text_container_dom"));
    document.execCommand("copy");
    window.clearSelection();
}
