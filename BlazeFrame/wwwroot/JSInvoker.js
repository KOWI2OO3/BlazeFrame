export function getProperty(obj, property) {
    return obj[property];
}

export function setProperty(obj, property, value) {
    obj[property] = value;
}

export function invokeFunction(obj, method, args) {
    if(obj != null) 
        return obj[method](...args);
    else 
        return this[method](...args);
}

export function invokeBatch(batchCalls) {
    for (let batchCall of batchCalls) {
        let params = batchCall.slice(3);
        switch (batchCall[0]) {
            case 'invokeFunction':
                invokeFunction(batchCall[1] ?? this, batchCall[2], params);
                break;
            case 'setProperty':
                setProperty(batchCall[1] ?? obj, batchCall[2], params[0]);
                break
        }
    }
}

export function getParentSize(obj)
{
    return {width: obj.parentElement.clientWidth, height: obj.parentElement.clientHeight};
}

export function print(message) {
    console.log(message);
}

export function showPrompt(message) {
    return prompt(message, 'Type anything here');
}

export function scaleContextToDisplay(ctx) {
    scaleToDisplay(ctx.canvas, ctx);
}

export function scaleCanvasToDisplay(canvas) {
    scaleToDisplay(canvas, canvas.getContext('2d'));
}

function scaleToDisplay(canvas, ctx) {
    // Get the DPR and size of the canvas
    const dpr = window.devicePixelRatio;
    const rect = canvas.getBoundingClientRect();
  
    // Set the "actual" size of the canvas
    canvas.width = rect.width * dpr;
    canvas.height = rect.height * dpr;
  
    // Scale the context to ensure correct drawing operations
    ctx.scale(dpr, dpr);
  
    // Set the "drawn" size of the canvas
    canvas.style.width = `${rect.width}px`;
    canvas.style.height = `${rect.height}px`;
}

/**
 * Converts the x y position of the mouse to the canvas coordinates
 * @param {canvas} canvas 
 * @param {number} x 
 * @param {number} y 
 * @returns {DOMPoint} The canvas coordinates
 */
export function mouseToCanvasCoordinates(canvas, x, y) {
    const ctx = canvas.getContext('2d');
    const matrix = ctx.getTransform();
    const inverseMatrix = matrix.inverse();

    let pos = getMousePos(canvas, x, y);
    
    const point = new DOMPoint(pos.x, pos.y);
    return point.matrixTransform(inverseMatrix);
}

function  getMousePos(canvas, x, y) {
    var rect = canvas.getBoundingClientRect(), // abs. size of element
      scaleX = canvas.width / rect.width,    // relationship bitmap vs. element for x
      scaleY = canvas.height / rect.height;  // relationship bitmap vs. element for y
  
    return {
      x: (x - rect.left) * scaleX,   // scale mouse coordinates after they have
      y: (y - rect.top) * scaleY     // been adjusted to be relative to element
    }
  }