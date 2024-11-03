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

function fillFacades(facades, params) 
{
    for(let i = 0; i < params.length; i++) 
    {
        if(params[i]['facadeId'])
            params[i] = facades[params[i]['facadeId']];
    }
    return params;
}

export function invokeBatch(batchCalls) {
    let results = {};
    let facades = {};
    for (let batchCall of batchCalls) {
        let params = batchCall.slice(3);

        params = fillFacades(facades, params);
        switch (batchCall[0]) {
            case 'invokeFunction':
                invokeFunction(batchCall[1] ?? this, batchCall[2], params);
                break;
            case 'setProperty':
                setProperty(batchCall[1] ?? obj, batchCall[2], params[0]);
                break
            case 'invokeCallbackFunction':
                let facade = batchCall[3];
                let facadeId = facade['facadeId'];

                let result = invokeFunction(batchCall[1] ?? this, batchCall[2], params.slice(1));

                facades[facadeId] = result;
                results[facadeId] = facade['requiresObjectReference'] ? DotNet.createJSObjectReference(result).__jsObjectId : result;
                break;
        }
    }
    return results;
}

export function compute(operation, a, b)
{
    switch(operation)
    {
        case 'add':
            return a + b;
        case 'subtract':
            return a - b;
        case 'multiply':
            return a * b;
        case 'divide':
            return a / b;
        case 'modulo':	
            return a % b;
        case 'negate':
            return -a;
        case 'invert':
            return !a;
        case 'equal':
            return a == b;
        case 'notequal':
            return a != b;
        case 'greater':
            return a > b;
        case 'less':
            return a < b;
        case 'gequal':
            return a >= b;
        case 'lequal':
            return a <= b;
        case 'and':
            return a & b;
        case 'or':
            return a | b;
        case 'xor':
            return a ^ b;
        case 'lshift':
            return a << b;
        case 'rshift':
            return a >> b;
        default:
            return 0;
    }
}

export function getParentSize(obj)
{
    return { width: obj.parentElement.clientWidth, height: obj.parentElement.clientHeight };
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

export function setUniform(context, program, name, type, value) 
{
    var location = context.getUniformLocation(program, name);
    context[type](location, value);
}

export function setMatrixUniform(context, program, name, type, transpose, value) 
{
    var location = context.getUniformLocation(program, name);
    context[type](location, transpose, value);
}

export function getValue()
{
    return 42;
    // return {
    //     interger: 42
    // };
}