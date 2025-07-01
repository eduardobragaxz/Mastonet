using Mastonet.Entities;
using System;

namespace Mastonet;

public class ServerErrorException(Error error) : Exception(error.Description)
{
}
