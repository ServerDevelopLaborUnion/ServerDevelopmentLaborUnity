import ws from 'ws';

const port = 32000;

const wsService = new ws.Server({ port }, () => {
    console.log(`Server is running on port ${port}`);
});


