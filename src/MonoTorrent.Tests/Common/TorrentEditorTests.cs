using MonoTorrent.BEncoding;
using NUnit.Framework;
using System;

namespace MonoTorrent.Common
{
    [TestFixture]
    public class TorrentEditorTests
    {
        [Test]
        public void EditingCreatesCopy()
        {
            BEncodedDictionary d = Create("comment", "a");
            TorrentEditor editor = new TorrentEditor(d);
            editor.Comment = "b";
            Assert.AreEqual("a", d["comment"].ToString(), "#1");
        }

        [Test]
        public void EditComment()
        {
            BEncodedDictionary d = Create("comment", "a");
            TorrentEditor editor = new TorrentEditor(d);
            editor.Comment = "b";
            d = editor.ToDictionary();
            Assert.AreEqual("b", d["comment"].ToString(), "#1");
        }

        [Test]
        public void ReplaceInfoDict()
        {
            Assert.Throws<InvalidOperationException>(
                     () =>
                     {
                         TorrentEditor editor = new TorrentEditor(new BEncodedDictionary()) { CanEditSecureMetadata = false };
                         editor.SetCustom("info", new BEncodedDictionary());
                     });
        }

        [Test]
        public void EditProtectedProperty_NotAllowed()
        {
            Assert.Throws<InvalidOperationException>(
                        () =>
                        {
                            TorrentEditor editor = new TorrentEditor(new BEncodedDictionary()) { CanEditSecureMetadata = false };
                            editor.PieceLength = 16;
                        });
        }

        private BEncodedDictionary Create(string key, string value)
        {
            BEncodedDictionary d = new BEncodedDictionary();
            d.Add(key, (BEncodedString)value);
            return d;
        }
    }
}

