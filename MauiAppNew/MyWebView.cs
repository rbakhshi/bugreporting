using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppNew
{
    public interface IMyWebView : IView
    {

    }

    public class MyWebView: View, IMyWebView
    {
    }
}
