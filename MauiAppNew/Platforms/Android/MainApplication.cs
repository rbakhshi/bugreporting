using Android.App;
using Android.Graphics;
using Android.Runtime;
using Android.Webkit;
using Java.Interop;
using JetBrains.Annotations;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;
using Object = Java.Lang.Object;
using WebView = Android.Webkit.WebView;

namespace MauiAppNew;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp(builder =>
    {
        // builder.ConfigureMauiHandlers(list => { list.AddHandler<MyWebView, MyWebViewHandler>(); });
        WebViewHandler.Mapper.AppendToMapping("SetJS",
            (handler, view) =>
            {
                handler.PlatformView.AddJavascriptInterface(new JsInterface(handler.PlatformView), "jsInterface");
            });
    });
}

class JsInterface : Java.Lang.Object, IValueCallback
{
    private readonly WebView _handlerPlatformView;

    public JsInterface(WebView handlerPlatformView)
    {
        _handlerPlatformView = handlerPlatformView;
    }

    [JavascriptInterface]
    [Export("Do")]
    public void Do()
    {
        MainThread.InvokeOnMainThreadAsync(() =>
        {
            _handlerPlatformView.EvaluateJavascript("alert('oooooo');", this);
            _handlerPlatformView.EvaluateJavascript("window.response('test')", this);
        });
    }

    public void OnReceiveValue(Object value)
    {
    }
}

public class MyWebViewHandler : ViewHandler<IMyWebView, WebView>
{
    public static PropertyMapper<IMyWebView, MyWebViewHandler> MyWebViewMapper = new(ViewHandler.ViewMapper);

    public MyWebViewHandler() : this(MyWebViewMapper)
    {
    }

    public MyWebViewHandler([NotNull] IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper,
        commandMapper)
    {
    }

    protected override WebView CreatePlatformView()
    {
        var view = new WebView(this.Context);
        view.SetWebViewClient(new MyWebViewClient());
        view.Settings.JavaScriptEnabled = true;
        view.Settings.AllowFileAccess = true;
        return view;
    }

    protected override void ConnectHandler(WebView platformView)
    {
        base.ConnectHandler(platformView);

        // var source = new UrlWebViewSource()
        // {
        //     Url = "index.html"
        // };
        // source.Load(new WebDelegate(platformView));
        platformView.LoadDataWithBaseURL("file:///android_asset/web/index.html",
            @"<html><script type=""text/javascript"">document.location = 'index.html';</script><body>Hello</html>",
            "text/html", "utf-8", null);
    }
}

public class WebDelegate : IWebViewDelegate
{
    private readonly WebView _view;

    public WebDelegate(WebView view)
    {
        _view = view;
    }

    public void LoadHtml(string html, string baseUrl)
    {
        _view.LoadDataWithBaseURL(baseUrl, html, "text/html", null, null);
    }

    public void LoadUrl(string url)
    {
        _view.LoadUrl(url);
    }
}

public class MyWebViewClient : WebViewClient
{
    public MyWebViewClient() //: base(handler)
    {
    }

    public override void OnReceivedError(WebView view, IWebResourceRequest request, WebResourceError error)
    {
        base.OnReceivedError(view, request, error);
        System.Diagnostics.Debug.WriteLine($"Error: {error.Description}");
    }

    public override void OnPageFinished(WebView view, string url)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(OnPageFinished)}: {url}");
        base.OnPageFinished(view, url);
    }

    public override void OnPageStarted(WebView view, string url, Bitmap favicon)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(OnPageStarted)}: {url}");
        base.OnPageStarted(view, url, favicon);
    }

    public override WebResourceResponse ShouldInterceptRequest(WebView view, IWebResourceRequest request)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(ShouldInterceptRequest)}: [{request.Method}] {request.Url}");
        var shouldInterceptRequest = base.ShouldInterceptRequest(view, request);
        return null;
    }

    public override bool ShouldOverrideUrlLoading(WebView view, IWebResourceRequest request)
    {
        System.Diagnostics.Debug.WriteLine($"{nameof(ShouldOverrideUrlLoading)}: [{request.Method}] {request.Url}");
        return false;
    }
}