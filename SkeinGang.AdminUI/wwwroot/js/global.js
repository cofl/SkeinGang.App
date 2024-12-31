/**
 * Enable an update button.
 * @param {number} [id]
 */
function enableUpdateButton(id) {
    const elementId = typeof id !== "undefined"
        ? `update_${id}`
        : "update";
    document.getElementById(elementId).disabled = false;
}

/**
 * Disable an update button.
 * @param {number?} [id]
 */
function disableUpdateButton(id) {
    const elementId = typeof id !== "undefined"
        ? `update_${id}`
        : "update";
    document.getElementById(elementId).disabled = true;
}

/**
 * @param {HTMLFormElement} form
 * @returns {Element | null}
 */
function getFormSubmit(form)
{
    return form.querySelector("input[type=submit]")
        || form.id && document.querySelector(`input[type=submit][form=${form.id}]`)
        || null
}

/**
 * @param {HTMLFormElement} form
 * @returns {HTMLInputElement[]}
 */
function getFormInputs(form)
{
    const inside = form.querySelectorAll("input")
    const outside = !form.id ? [] : document.querySelectorAll(`input[form=${form.id}]`)
    return [
        ...inside,
        ...outside,
    ].filter(element => !element.disabled && element.type !== "hidden" && element.type !== "submit")
}

/**
 * @param {HTMLFormElement} form
 * @returns {HTMLSelectElement[]}
 */
function getFormSelects(form)
{
    const inside = form.querySelectorAll("select")
    const outside = !form.id ? [] : document.querySelectorAll(`select[form=${form.id}]`)
    return [
        ...inside,
        ...outside,
    ].filter(element => !element.disabled)
}

window.onload = () => {
    /** @type {NodeListOf<HTMLFormElement>} */
    const forms = document.querySelectorAll("form[data-model-id]");
    for (const form of forms){
        /** @type {HTMLSelectElement|null} */
        const submit = getFormSubmit(form)
        if (!submit) continue;
        submit.disabled = true;

        form.onsubmit = () => setTimeout(() => submit.disabled = true, 0)
        for (const field of getFormInputs(form))
            field.oninput = () => submit.disabled = false;
        for (const field of getFormSelects(form))
            field.onchange = () => submit.disabled = false;
    }
}
