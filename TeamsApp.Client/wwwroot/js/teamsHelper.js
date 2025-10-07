window.getTeamsContext = async function () {
    if (window.microsoftTeams) {
        try {
            await microsoftTeams.app.initialize();
            const context = await microsoftTeams.app.getContext();
            return {
                userPrincipalName: context.user?.userPrincipalName || "",
                displayName: context.user?.displayName || ""
            };
        } catch (e) {
            console.error("Error getting Teams context:", e);
            return null;
        }
    } else {
        return null;
    }
};

window.getPublicIP = async function () {
    try {
        const response = await fetch("https://api.ipify.org?format=json");
        const data = await response.json();
        return data.ip;
    } catch (e) {
        console.error("Failed to get IP:", e);
        return "Unknown";
    }
};

window.openFileSmart = async function (url) {
    if (window.microsoftTeams) {
        try {
            await microsoftTeams.app.initialize();
            microsoftTeams.openLink(url);
        } catch (e) {
            console.error("Teams openLink failed, falling back:", e);
            window.open(url, "_blank");
        }
    } else {
        window.open(url, "_blank");
    }
};
