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
