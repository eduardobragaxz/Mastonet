using Mastonet.Entities;
using System;

namespace Mastonet;

public sealed class ServerErrorException(Error error) : Exception(error.Description)
{
}
