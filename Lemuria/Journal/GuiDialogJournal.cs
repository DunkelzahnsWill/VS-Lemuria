using System;
using System.Collections.Generic;
using Lemuria.Core;
using Vintagestory.API.Client;
using Vintagestory.API.Config;

namespace Lemuria.Journal;

/// <summary>
/// The Journal's own book screen: a chapter list on the left and an open spread of two pages of rich
/// text, turned with the buttons below or the arrow keys.
/// </summary>
public class GuiDialogJournal : GuiDialog
{
    private const int ListWidth = 170;
    private const int PageWidth = 290;
    private const int PageHeight = 380;
    private const int Gap = 20;

    private readonly List<JournalChapter> chapters;
    private int chapterIndex;
    /// <summary>The left page of the open spread, counted from 0; always even.</summary>
    private int leftPage;

    public override string ToggleKeyCombinationCode => null!;

    public GuiDialogJournal(ICoreClientAPI capi, List<JournalChapter> chapters) : base(capi)
    {
        this.chapters = chapters;
        Compose();
    }

    private static string L(string key) => Lang.Get(LemuriaModSystem.ModId + ":" + key);

    private JournalChapter? Chapter => chapterIndex < chapters.Count ? chapters[chapterIndex] : null;

    private string PageText(int page)
    {
        var chapter = Chapter;
        if (chapter == null || page < 0 || page >= chapter.Pages)
        {
            return "";
        }
        return L($"journal-{chapter.Code}-page{page + 1}");
    }

    private void Compose()
    {
        var chapter = Chapter;
        int pages = chapter?.Pages ?? 0;
        var font = CairoFont.WhiteSmallishText();

        var listBounds = ElementBounds.Fixed(0, 30, ListWidth, PageHeight);
        var leftBounds = ElementBounds.Fixed(ListWidth + Gap, 30, PageWidth, PageHeight);
        var rightBounds = ElementBounds.Fixed(ListWidth + 2 * Gap + PageWidth, 30, PageWidth, PageHeight);
        var prevBounds = ElementBounds.Fixed(ListWidth + Gap, 40 + PageHeight, 60, 30);
        var labelBounds = ElementBounds.Fixed(ListWidth + Gap + 70, 45 + PageHeight, 2 * PageWidth + Gap - 140, 30);
        var nextBounds = ElementBounds.Fixed(ListWidth + 2 * Gap + 2 * PageWidth - 60, 40 + PageHeight, 60, 30);

        var bgBounds = ElementBounds.Fill.WithFixedPadding(GuiStyle.ElementToDialogPadding);
        bgBounds.BothSizing = ElementSizing.FitToChildren;
        bgBounds.WithChildren(listBounds, leftBounds, rightBounds, prevBounds, labelBounds, nextBounds);
        var dialogBounds = ElementStdBounds.AutosizedMainDialog.WithAlignment(EnumDialogArea.CenterMiddle);

        var composer = capi.Gui
            .CreateCompo(LemuriaModSystem.ModId + "-journal", dialogBounds)
            .AddShadedDialogBG(bgBounds)
            .AddDialogTitleBar(L("journal-screen-title"), () => TryClose())
            .BeginChildElements(bgBounds);

        for (int i = 0; i < chapters.Count; i++)
        {
            int index = i;
            var buttonBounds = ElementBounds.Fixed(0, 30 + i * 36, ListWidth, 30);
            listBounds.WithChild(buttonBounds);
            composer.AddSmallButton(L($"journal-{chapters[i].Code}-title"), () => OpenChapter(index), buttonBounds,
                i == chapterIndex ? EnumButtonStyle.Normal : EnumButtonStyle.Small, "chapter" + i);
        }

        composer
            .AddRichtext(PageText(leftPage), font, leftBounds, "leftpage")
            .AddRichtext(PageText(leftPage + 1), font, rightBounds, "rightpage")
            .AddIf(leftPage > 0)
                .AddSmallButton("<", PreviousSpread, prevBounds)
            .EndIf()
            .AddStaticText(SpreadLabel(pages), CairoFont.WhiteSmallText().WithOrientation(EnumTextOrientation.Center), labelBounds)
            .AddIf(leftPage + 2 < pages)
                .AddSmallButton(">", NextSpread, nextBounds)
            .EndIf()
            .EndChildElements();

        SingleComposer = composer.Compose();
    }

    private string SpreadLabel(int pages)
    {
        if (pages == 0)
        {
            return "";
        }
        int last = Math.Min(leftPage + 2, pages);
        return last == leftPage + 1 ? $"{leftPage + 1} / {pages}" : $"{leftPage + 1}-{last} / {pages}";
    }

    private bool OpenChapter(int index)
    {
        chapterIndex = index;
        leftPage = 0;
        Compose();
        return true;
    }

    private bool PreviousSpread()
    {
        if (leftPage > 0)
        {
            leftPage -= 2;
            Compose();
        }
        return true;
    }

    private bool NextSpread()
    {
        if (leftPage + 2 < (Chapter?.Pages ?? 0))
        {
            leftPage += 2;
            Compose();
        }
        return true;
    }

    public override void OnKeyDown(KeyEvent args)
    {
        base.OnKeyDown(args);
        if (args.Handled)
        {
            return;
        }
        if (args.KeyCode == (int)GlKeys.Left || args.KeyCode == (int)GlKeys.PageUp)
        {
            PreviousSpread();
            args.Handled = true;
        }
        else if (args.KeyCode == (int)GlKeys.Right || args.KeyCode == (int)GlKeys.PageDown)
        {
            NextSpread();
            args.Handled = true;
        }
    }
}
