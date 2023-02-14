using UnityEngine;

// Source: https://forum.unity.com/threads/strip-release-build-from-all-debug-log-calls.353600/#post-2292960
public sealed class Logger {
  public const string INFO    = "LOG_INFO";
  public const string WARNING = "LOG_WARNING";
  public const string ERROR   = "LOG_ERROR";

  // m -> message,
  // c -> context,
  // a -> args
  [System.Diagnostics.Conditional(INFO)]
  public static void Log(object m) { Debug.Log(m); }

  [System.Diagnostics.Conditional(INFO)]
  public static void Log(object m, Object c) { Debug.Log(m, c); }

  [System.Diagnostics.Conditional(INFO)]
  public static void LogFormat(string m, params object[] a) { Debug.LogFormat(m, a); }

  [System.Diagnostics.Conditional(INFO)]
  public static void LogFormat(Object c, string m, params object[] a) { Debug.LogFormat(c, m, a); }

  [System.Diagnostics.Conditional(WARNING)]
  public static void LogWarning(object m) { Debug.LogWarning(m); }

  [System.Diagnostics.Conditional(WARNING)]
  public static void LogWarning(object m, Object c) { Debug.LogWarning(m, c); }

  [System.Diagnostics.Conditional(WARNING)]
  public static void LogWarningFormat(string m, params object[] a) { Debug.LogWarningFormat(m, a); }

  [System.Diagnostics.Conditional(WARNING)]
  public static void LogWarningFormat(Object c, string m, params object[] a) { Debug.LogWarningFormat(c, m, a); }

  [System.Diagnostics.Conditional(ERROR)]
  public static void LogError(object m) { Debug.LogError(m); }

  [System.Diagnostics.Conditional(ERROR)]
  public static void LogError(object m, Object c) { Debug.LogError(m, c); }

  [System.Diagnostics.Conditional(ERROR)]
  public static void LogErrorFormat(string m, params object[] a) { Debug.LogErrorFormat(m, a); }

  [System.Diagnostics.Conditional(ERROR)]
  public static void LogErrorFormat(Object c, string m, params object[] a) { Debug.LogErrorFormat(c, m, a); }
}

