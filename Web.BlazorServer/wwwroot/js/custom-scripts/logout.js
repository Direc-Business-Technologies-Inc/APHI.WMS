async function LogoutAPI(uri) {
    try {
        for (let i = localStorage.length - 1; i >= 0; i--) {
            const key = localStorage.key(i);
            if (key && key.endsWith('-TSET')) {
                localStorage.removeItem(key);
            }
        }

        const response = await fetch(uri, {
            method: "GET",
            credentials: "include",
        });

        if (!response.ok) {
            console.error("Logout failed");
            return false;
        }
        return true;
    }
    catch (error) {
        console.error(error);
        return false;
    }
}
