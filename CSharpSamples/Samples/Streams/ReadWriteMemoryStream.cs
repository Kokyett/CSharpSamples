namespace CSharpSamples.Samples.Streams;

public sealed class ReadWriteMemoryStream : MemoryStream
{
    private readonly object _locker = new ();
    private long _readPosition;
    private long _writePosition;
    private readonly AutoResetEvent _waiter = new(false);
    
    public override bool CanTimeout { get; }
    public override int ReadTimeout { get; set; }

    public ReadWriteMemoryStream(bool canTimeout = false, int timeout = Timeout.Infinite)
    {
        CanTimeout = canTimeout;
        ReadTimeout = timeout;
    }
    
    private int ReadWithLock(byte[] buffer, int offset, int count) {
        int result;
        lock (_locker) {
            Position = _readPosition;
            result = base.Read(buffer, offset, count);
            _readPosition += result;
        }
        return result;
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        if (!CanTimeout)
            return ReadWithLock(buffer, offset, count);

        int result;
        while((result = ReadWithLock(buffer, offset, count)) == 0) {
            if (!_waiter.WaitOne(ReadTimeout))
                throw new TimeoutException();
        }
        return result;
    }
    
    public override void Write(byte[] buffer, int offset, int count)
    {
        lock (_locker)
        {
            Position = _writePosition;
            base.Write(buffer, offset, count);
            _writePosition = count;
        }
        _waiter.Set();
    }
}