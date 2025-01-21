namespace CSharpSamples.Samples.Streams;

public class ReadWriteMemoryStream(int timeout = Timeout.Infinite, bool throwExceptionOnTimeOut = true) : MemoryStream
{
    private long _readPosition;
    private long _writePosition;
    private readonly Lock _lock= new();
    private readonly AutoResetEvent _event = new(false);

    public override void Write(byte[] buffer, int offset, int count)
    {
        lock (_lock)
        {
            Position = _writePosition;
            base.Write(buffer, offset, count);
            _writePosition = Position;
            _event.Set();
        }
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (!_event.WaitOne(timeout))
        {
            if (throwExceptionOnTimeOut)
                throw new TimeoutException();
            return 0;
        }
        lock (_lock)
        {
            Position = _readPosition;
            var result = base.Read(buffer, offset, count);
            _readPosition += result;
            return result;
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _event.Dispose();
    }
}