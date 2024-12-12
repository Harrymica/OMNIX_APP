function loginWithTelegram() {
        const telegramLoginButton = document.createElement('script');
        telegramLoginButton.src = "https://telegram.org/js/telegram-widget.js?7";
        telegramLoginButton.setAttribute('data-telegram-login', 'YOUR_BOT_USERNAME');
        telegramLoginButton.setAttribute('data-size', 'large');
        telegramLoginButton.setAttribute('data-radius', '10');
        telegramLoginButton.setAttribute('data-auth-url', '/your-auth-url'); // Your server-side auth endpoint
        telegramLoginButton.setAttribute('data-request-access', 'write'); // Optional permissions
        document.body.appendChild(telegramLoginButton);
    }

    function onTelegramAuth(user) {
        DotNet.invokeMethodAsync('YourAssemblyName', 'ReceiveTelegramUserInfo', user);
    }
