function parseBuffer(msg)
{
    return { type, payload } = JSON.parse(msg);
}

exports.parseBuffer = parseBuffer;