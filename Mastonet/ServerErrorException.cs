using Mastonet.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mastonet;

public class ServerErrorException(Error error) : Exception(error.Description)
{
}
