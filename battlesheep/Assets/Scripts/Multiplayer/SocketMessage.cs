using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SocketMessage
{
    public enum Code
    {
        Login = 1,
        LoginResponse = 2,

        Register = 3,
        RegisterResponse = 4,

        SendInitialBoard = 5,

        StartGame = 7
    }

    private List<byte> _data;

    private Code _command;
    public Code Command { get { return _command; } }

    public SocketMessage(byte[] data)
    {
        _data = data.ToList();

        int code = data[0];
        _command = (Code)code;
    }

    public SocketMessage(Code command)
    {
        _command = command;

        _data = new List<byte>();
        AddByte((byte)command);
    }

    public void AddByte(byte data)
    {
        _data.Add(data);
    }

    public void AddChar(char data)
    {
        foreach (var temp in BitConverter.GetBytes(data))
            _data.Add(temp);
    }

    public void AddBool(bool data)
    {
        if (data)
            AddByte(1);
        else
            AddByte(0);
    }

    private bool ContainsData(int index, int size)
    {
        return index >= 0 && size >= 1 && index <= (_data.Count - size + 1);
    }

    public bool GetBool(int index)
    {
        Debug.Assert(ContainsData(index, 1));
        return _data[index] == 0 ? false : true;        
    }

    public byte GetByte(int index)
    {
        Debug.Assert(ContainsData(index, 1));
        return _data[index];
    }

    public char GetChar(int index)
    {
        return (char)GetByte(index);
    }

    public string GetString(int index, int size)
    {
        Debug.Assert(ContainsData(index, size));
        
        var values = new char[size];
        for (int i = 0; i < size; ++i)
            values[i] = GetChar(index);

        return new string(values);
    }

    public void AddString(string text, int length)
    {
        var bytes = Encoding.UTF8.GetBytes(text);

        for (int i = 0; i < length; ++i)
        {
            if (i < bytes.Length)
                AddByte(bytes[i]);
            else
                AddByte(0);
        }
    }

    public byte[] GetData()
    {
        return _data.ToArray();
    }
}