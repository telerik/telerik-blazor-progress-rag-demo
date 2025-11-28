// Handle Enter key in search input to submit instead of adding new line
window.searchInputInterop = {
    initialize: function (element, dotNetRef) {
        if (!element) return;
        
        const textarea = element.querySelector('textarea');
        if (!textarea) return;
        
        textarea.addEventListener('keydown', function (e) {
            if (e.key === 'Enter' && !e.shiftKey) {
                e.preventDefault();
                dotNetRef.invokeMethodAsync('OnEnterPressed');
            }
        });
    }
};

// Copy code block functionality
window.copyCodeBlock = async function (button) {
    const codeBlock = button.closest('.code-block-container');
    if (!codeBlock) return;
    
    const pre = codeBlock.querySelector('pre');
    if (!pre) return;
    
    const code = pre.textContent;
    
    try {
        await navigator.clipboard.writeText(code);
        
        // Update button to show "Copied!" state
        const originalContent = button.innerHTML;
        button.innerHTML = '<svg viewBox="0 0 512 512" focusable="false" class="k-svg-icon k-svg-i-copy" role="presentation" style="width: 16px; height: 16px;"><path d="M416 480H160c-17.7 0-32-14.3-32-32V160h-32c-17.7 0-32 14.3-32 32v288c0 17.7 14.3 32 32 32h288c17.7 0 32-14.3 32-32z"></path><path d="M384 32H128c-17.7 0-32 14.3-32 32v320c0 17.7 14.3 32 32 32h256c17.7 0 32-14.3 32-32V64c0-17.7-14.3-32-32-32m-32 288H192c-17.7 0-32-14.3-32-32s14.3-32 32-32h160c17.7 0 32 14.3 32 32s-14.3 32-32 32m0-96H192c-17.7 0-32-14.3-32-32s14.3-32 32-32h160c17.7 0 32 14.3 32 32s-14.3 32-32 32m0-96H192c-17.7 0-32-14.3-32-32s14.3-32 32-32h160c17.7 0 32 14.3 32 32s-14.3 32-32 32"></path></svg><span class="code-copy-text">Copied!</span>';
        button.classList.add('copied');
        
        // Reset after 2 seconds
        setTimeout(() => {
            button.innerHTML = originalContent;
            button.classList.remove('copied');
        }, 2000);
    } catch (err) {
        console.error('Failed to copy code:', err);
    }
};
