﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.VisualStudio.IntegrationTest.Utilities.Common;
using Microsoft.VisualStudio.IntegrationTest.Utilities.InProcess;

namespace Microsoft.VisualStudio.IntegrationTest.Utilities.OutOfProcess
{
    /// <summary>
    /// Provides a means of interacting with the Visual Studio editor by remoting calls into Visual Studio.
    /// </summary>
    public partial class Editor_OutOfProc : OutOfProcComponent
    {
        private readonly Editor_InProc _inProc;

        internal Editor_OutOfProc(VisualStudioInstance visualStudioInstance)
            : base(visualStudioInstance)
        {
            _inProc = CreateInProcComponent<Editor_InProc>(visualStudioInstance);
        }

        public void Activate()
            => _inProc.Activate();

        public string GetText()
            => _inProc.GetText();

        public void SetText(string value)
            => _inProc.SetText(value);

        public string GetCurrentLineText()
            => _inProc.GetCurrentLineText();

        public int GetCaretPosition()
            => _inProc.GetCaretPosition();

        public string GetLineTextBeforeCaret()
            => _inProc.GetLineTextBeforeCaret();

        public string GetLineTextAfterCaret()
            => _inProc.GetLineTextAfterCaret();

        public void MoveCaret(int position)
            => _inProc.MoveCaret(position);

        public void PlaceCaret(string marker, int charsOffset, int occurrence, bool extendSelection, bool selectBlock)
            => _inProc.PlaceCaret(marker, charsOffset, occurrence, extendSelection, selectBlock);

        public string[] GetCompletionItems()
        {
            WaitForCompletionSet();
            return _inProc.GetCompletionItems();
        }

        public string GetCurrentCompletionItem()
        {
            WaitForCompletionSet();
            return _inProc.GetCurrentCompletionItem();
        }

        public bool IsCompletionActive()
        {
            WaitForCompletionSet();
            return _inProc.IsCompletionActive();
        }

        public bool IsSignatureHelpActive()
        {
            WaitForSignatureHelp();
            return _inProc.IsSignatureHelpActive();
        }

        public Signature[] GetSignatures()
        {
            WaitForSignatureHelp();
            return _inProc.GetSignatures();
        }

        public Signature GetCurrentSignature()
        {
            WaitForSignatureHelp();
            return _inProc.GetCurrentSignature();
        }

        public void ShowLightBulb()
            => _inProc.ShowLightBulb();

        public void WaitForLightBulbSession()
            => _inProc.WaitForLightBulbSession();

        public void DismissLightBulbSession()
            => _inProc.DismissLightBulbSession();

        public bool IsLightBulbSessionExpanded()
            => _inProc.IsLightBulbSessionExpanded();

        public string[] GetLightBulbActions()
            => _inProc.GetLightBulbActions();

        public void ApplyLightBulbAction(string action, FixAllScope? fixAllScope, bool blockUntilComplete = true)
            => _inProc.ApplyLightBulbAction(action, fixAllScope, blockUntilComplete);

        public bool IsCaretOnScreen()
            => _inProc.IsCaretOnScreen();

        public void AddWinFormButton(string buttonName)
            => _inProc.AddWinFormButton(buttonName);

        public void DeleteWinFormButton(string buttonName)
            => _inProc.DeleteWinFormButton(buttonName);

        public void EditWinFormButtonProperty(string buttonName, string propertyName, string propertyValue, string propertyTypeName = null)
            => _inProc.EditWinFormButtonProperty(buttonName, propertyName, propertyValue, propertyTypeName);

        public void EditWinFormButtonEvent(string buttonName, string eventName, string eventHandlerName)
            => _inProc.EditWinFormButtonEvent(buttonName, eventName, eventHandlerName);

        public string GetWinFormButtonPropertyValue(string buttonName, string propertyName)
            => _inProc.GetWinFormButtonPropertyValue(buttonName, propertyName);

        /// <summary>
        /// Sends key strokes to the active editor in Visual Studio. Various types are supported by this method:
        /// <see cref="string"/> (each character will be sent separately, <see cref="char"/>, <see cref="VirtualKey"/>
        /// and <see cref="KeyPress"/>.
        /// </summary>
        public void SendKeys(params object[] keys)
        {
            Activate();
            VisualStudioInstance.SendKeys.Send(keys);
        }

        public void MessageBox(string message)
            => _inProc.MessageBox(message);

        public void VerifyDialog(string dialogName, bool isOpen)
            => _inProc.VerifyDialog(dialogName, isOpen);

        public void PressDialogButton(string dialogAutomationName, string buttonAutomationName)
            => _inProc.PressDialogButton(dialogAutomationName, buttonAutomationName);

        public void DialogSendKeys(string dialogAutomationName, string keys)
            => _inProc.DialogSendKeys(dialogAutomationName, keys);

        public void Undo()
            => _inProc.Undo();
    }
}