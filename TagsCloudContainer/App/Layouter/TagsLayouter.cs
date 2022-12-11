﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.App.Layouter;
using TagsCloudContainer.Infrastructure;

namespace TagsCloudContainer.App.Layouter
{
    public class TagsLayouter
    {
        private ICircularCloudLayouter cloudLayouter;
        private readonly ITagsExtractor readTags;
        private readonly IImageHolder imageHolder;
        private readonly FontText fontText;

        public TagsLayouter(ICircularCloudLayouter cloudLayouter, ITagsExtractor readTags,
            FontText fontText, IImageHolder imageHolder)
        {
            this.cloudLayouter = cloudLayouter;
            this.readTags = readTags;
            this.imageHolder = imageHolder;
            this.fontText = fontText;
        }

        public List<TagInfo> PutAllTags()
        {
            if (readTags.Text is null || readTags.Text.Count == 0) return null;
            var minT = readTags.Text.Values.Min();
            var maxT = readTags.Text.Values.Max();
            var tags = readTags.Text
                .Select(t => new TagInfo(t.Key, new Font(fontText.Font.FontFamily,
                    CalculateSizeFont(t.Value, minT, maxT, fontText.Font.Size), fontText.Font.Style)))
                .ToList();

            foreach (var tag in tags)
            {
                var sizeF = CalculateSizeWord(tag.TagText, tag.TagFont);
                var size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));
                var rect = cloudLayouter.PutNextRectangle(size);
                tag.TagRect = rect;
            }
            cloudLayouter.Clear();
            return tags;
        }

        private float CalculateSizeFont(int T, int minT, int maxT, float f)
        {
            if (maxT == minT) return f;
            return Math.Max(f * (T - minT) / (maxT - minT), 1);
        }

        private SizeF CalculateSizeWord(string word, Font font)
        {
            var gph = imageHolder.StartDrawing();
            return gph.MeasureString(word, font);
        }
    }
}
