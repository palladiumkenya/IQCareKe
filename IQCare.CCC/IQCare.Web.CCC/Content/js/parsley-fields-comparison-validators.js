//  Not equal to validator
window.ParsleyValidator.addValidator('notequalto', 
    function (value, requirement) {
        return value !== $(requirement).val();
    }, 32)
    .addMessage('en', 'notequalto', 'This value should not be the same.');

// Greater than validator
window.ParsleyValidator.addValidator('gt', 
	function (value, requirement) {
        return parseFloat(value) > parseFloat($(requirement).val());
    }, 32)
    .addMessage('en', 'gt', 'This value should be greater');

// Greater than or equal to validator
window.ParsleyValidator.addValidator('ge', 
	function (value, requirement) {
        return parseFloat(value) >= parseFloat($(requirement).val());
    }, 32)
    .addMessage('en', 'ge', 'This value should be greater or equal');

// Less than validator
window.ParsleyValidator.addValidator('lt', 
	function (value, requirement) {
        return parseFloat(value) < parseFloat($(requirement).val());
    }, 32)
    .addMessage('en', 'lt', 'This value should be less');

// Less than or equal to validator
window.ParsleyValidator.addValidator('le', 
	function (value, requirement) {
        return parseFloat(value) <= parseFloat($(requirement).val());
    }, 32)
    .addMessage('en', 'le', 'This value should be less or equal');
