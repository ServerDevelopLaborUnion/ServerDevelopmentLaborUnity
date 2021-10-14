function parseBuffer(msg)
{
    const { type, payload } = JSON.parse(msg);
    return { type, payload };
}

exports.parseBuffer = parseBuffer;