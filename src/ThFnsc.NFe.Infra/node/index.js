var args = process.argv.slice(2);
if (args.length != 2)
    throw "Exactly two parameters are needed";

async function printPDF() {
    try {
        const util = require('util');
        const exec = util.promisify(require('child_process').exec);

        console.log("Ensuring dependencies are installed...");
        await exec("npm i", { cwd: __dirname });

        console.log("Loading dependencies...");
        const puppeteer = require('puppeteer-core');
        const fs = require("fs/promises");
        const path = require("path");
        const chromePaths = require('chrome-paths');
        var browser;

        console.log("Lauching browser...");
        for (var chromeLocation of [undefined, ...Object.values(chromePaths)])
            try {
                browser = await puppeteer.launch({ executablePath: chromeLocation, headless: true, args: ["--no-sandbox", "--disable-setuid-sandbox"] });
                break;
            } catch (e) {
                console.error(e);
            }

        console.log("Opening new tab...");
        const page = await browser.newPage();

        console.log("Opening file...");
        await page.goto(`file://${path.resolve(args[0])}`);

        console.log("Rendering to PDF...");
        const pdf = await page.pdf({ format: 'A4' });

        console.log("Saving PDF to file...");
        await fs.writeFile(args[1], pdf);

        console.log("Closing browser...");
        await browser.close();

        console.log("All done!");
    } catch (e) {
        console.error(e);
        process.exit(1);
    }
}

printPDF();