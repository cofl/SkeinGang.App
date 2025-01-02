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
 * @param {EventTarget} target
 * @returns {HTMLFormElement | null}
 */
function relatedForm(target) {
    if (!(target instanceof HTMLInputElement && target.type !== "hidden" && target.type !== "submit")
        && !(target instanceof HTMLSelectElement)
        && !(target instanceof HTMLButtonElement))
        return null

    const form = target.form
    if (!form || !form.hasAttribute("data-model-id"))
        return null

    return form
}

window.onchange = window.oninput = (event) => {
    const form = relatedForm(event.target)
    if (!form) return

    const submit = Array.from(form.elements)
        .find(element => element.type === "submit")
    if (submit) submit.disabled = false
}

window.onsubmit = (event) => {
    if (!(event.target instanceof HTMLFormElement) || !event.target.hasAttribute("data-model-id"))
        return

    const submit = Array.from(event.target.elements)
        .find(element => element.type === "submit")
    if (submit) setTimeout(() => submit.disabled = true, 0)
}
