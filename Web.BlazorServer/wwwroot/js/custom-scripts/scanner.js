window.dotnetInstance;

export function setdotnetInstance(dotnetInstance) {
    window.dotnetInstance = dotnetInstance;
}

const SCANNER_STATES = {
    IDLE: 0,
    SCAN: 1,
    DISPLAY: 2,
    REVIEW: 3,
    PROMPT: 4
};

class Scanner {
    constructor() {

        this.state = {
            currentState: SCANNER_STATES.IDLE,
            scanBuffer: '',
            scanTimeout: null
        };

        this.handleKeyPress = this.handleKeyPress.bind(this);
    }

    initialize() {
        this.resetToIdle();
        console.log("[INITIALIZED][IDLE] SCANNER is IDLE");
    }

    handleKeyPress(event) {
        
        if (this.state.currentState !== SCANNER_STATES.SCAN) return;
        
        if (this.state.scanTimeout) {
            clearTimeout(this.state.scanTimeout);
        }
        
        this.state.scanBuffer += event.key;
        console.log("[SCAN] SCANNER: scanned ", this.state.scanBuffer);

        this.state.scanTimeout = setTimeout(() => {
            if (this.state.scanBuffer) {
                this.processScan(this.state.scanBuffer);
                this.state.scanBuffer = '';
            }
        }, 100);
    }

    processScan(scannedData) {
        console.log("[SCAN] DISPLAY: ", scannedData);
        dotnetInstance.invokeMethodAsync('UpdateScannedData', scannedData)
        
        this.state.currentState = SCANNER_STATES.DISPLAY;
        dotnetInstance.invokeMethodAsync('UpdateState', SCANNER_STATES.DISPLAY)
    }

    startScan() {
        document.addEventListener('keypress', (e) => this.handleKeyPress(e));
        this.state.currentState = SCANNER_STATES.SCAN;
        dotnetInstance.invokeMethodAsync('UpdateState', SCANNER_STATES.SCAN)
    }

    restartScan() {
        this.state.scanBuffer = '';
        this.state.currentState = SCANNER_STATES.SCAN;
        dotnetInstance.invokeMethodAsync('UpdateState', SCANNER_STATES.SCAN)
    }

    promptNextScan() {
        this.state.currentState = SCANNER_STATES.PROMPT;
        dotnetInstance.invokeMethodAsync('UpdateState', SCANNER_STATES.PROMPT)
    }

    resetToIdle() {
        this.state.currentState = SCANNER_STATES.IDLE;
        dotnetInstance.invokeMethodAsync('UpdateState', SCANNER_STATES.IDLE)
    }

    destroy() {
        document.removeEventListener('keypress', this.handleKeyPress);
    }
}

let scanner = null;

export function initializeScanner() {
    scanner = new Scanner();
};

export function startScan()
{
    if(scanner) {
        scanner.startScan();
    }
}

export function promptNextScan()
{
    if(scanner) {
        scanner.promptNextScan();
    }
}

export function restartScan()
{
    if(scanner) {
        scanner.restartScan();
    }
}


export function resetToIdle()
{
    if(scanner) {
        scanner.resetToIdle();
    }
}

export function test() {
    const scanner = new Scanner();
    scanner.initialize();
};